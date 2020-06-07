using System;
using System.Net.Http.Headers;

namespace Codenation.Challenge
{
    public class CesarCypher : ICrypt, IDecrypt
    {
        private const int steps = 3;
        private const int alphabet = 26;
        private const int ascCodeInitLessSteps = 94;
        private const int ascCodeInit = 97;
        private const int ascCodeEnd = 122;
        private const int ascCodeEndAddedSteps = 125;

        public string Crypt(string message)
        {
            return Coding(message, "Crypt");
        }

        public string Decrypt(string cryptedMessage)
        {
            return Coding(cryptedMessage, "Decrypt");
        }

        public string Coding(string message, string operation)
        {
            //Using ternary conditional operator
            string cypher = message is null ? throw new ArgumentNullException() : "";
            var cypherLowerCase = message.ToLower();

            for (int cont = 0; cont < cypherLowerCase.Length; cont++)
            {
                //Variable where we set up the ascii code
                int constructor = 0;
                int character = cypherLowerCase[cont];

                if (char.IsWhiteSpace(Convert.ToChar(character)) || char.IsNumber(Convert.ToChar(character)))
                {
                    constructor = character;
                }
                else if (operation == "Decrypt")
                {
                    if (character - steps >= ascCodeInitLessSteps && character - steps < ascCodeInit)
                    {
                        constructor = (character - steps) + alphabet;
                    }
                    else if (character >= ascCodeInit && character <= ascCodeEnd)
                    {
                        constructor = character - steps;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }
                else if (operation == "Crypt")
                {
                    if (character + steps > ascCodeEnd && character + steps <= ascCodeEndAddedSteps)
                    {
                        constructor = (character + steps) - alphabet;
                    }
                    else if (character >= ascCodeInit && character <= ascCodeEnd)
                    {
                        constructor = character + steps;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                  
                }
                //convert ascii code to character
                cypher += char.ConvertFromUtf32(constructor);
            }
            return cypher;
        }

    }
}
