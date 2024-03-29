﻿using System;
using System.Collections.Generic;

namespace Kentor.AuthServices.WebSso
{
    /// <summary>
    /// Factory to create the command objects thand handles the incoming http requests.
    /// </summary>
    public static class CommandFactory
    {
        private static readonly ICommand notFoundCommand = new NotFoundCommand();

        /// <summary>
        /// The name of the Assertion Consumer Service Command.
        /// </summary>
        public const string AcsCommandName = "Acs";

        /// <summary>
        /// The name of the Sign In Command.
        /// </summary>
        public const string SignInCommandName = "SignIn";

        /// <summary>
        /// The metadata command has no name - it is triggered at base url for
        /// AuthServices.
        /// </summary>
        public const string MetadataCommand = "";

        private static readonly IDictionary<string, ICommand> commands =
        new Dictionary<string, ICommand>(StringComparer.OrdinalIgnoreCase) 
        { 
            { SignInCommandName, new SignInCommand() },
            { AcsCommandName, new AcsCommand() },
            { MetadataCommand, new MetadataCommand() },
        };

        /// <summary>
        /// Gets a command for a command name.
        /// </summary>
        /// <param name="commandName">Name of a command. Probably a path. A
        /// leading slash in the command name is ignored.</param>
        /// <returns>A command implementation or notFoundCommand if invalid.</returns>
        public static ICommand GetCommand(string commandName)
        {
            ICommand command;

            if(commandName ==  null)
            {
                throw new ArgumentNullException("commandName");
            }

            if(commandName.StartsWith("/", StringComparison.OrdinalIgnoreCase))
            {
                commandName = commandName.Substring(1);
            }

            if (commands.TryGetValue(commandName, out command))
            {
                return command;
            }

            return notFoundCommand;
        }
    }
}
