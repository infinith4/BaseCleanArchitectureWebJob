using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.Core.Constants
{
    public class EventLog
    {
        /// <summary>
        /// モジュールID
        /// </summary>
        /// <remarks>下三桁は0</remarks>
        public class Module
        {
            /// <summary>
            /// Core
            /// </summary>
            public static int Core = 1000;
        }

        /// <summary>
        /// UseCase Id
        /// </summary>
        /// <remarks>下一桁は0</remarks>
        public class UseCase
        {
            /// <summary>
            /// UseCase1
            /// </summary>
            public static int UseCase1 = 10;
            /// <summary>
            /// UseCase2
            /// </summary>
            public static int UseCase2 = 20;
            /// <summary>
            /// UseCase3
            /// </summary>
            public static int UseCase3 = 30;
            /// <summary>
            /// UseCase4
            /// </summary>
            public static int UseCase4 = 40;
            /// <summary>
            /// UseCase5
            /// </summary>
            public static int UseCase5 = 50;
        }
    }
}
