using System;
﻿using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
			MyActorSystem = ActorSystem.Create("MyActorSystem");

			Props consoleWriterProps = Props.Create(() => new ConsoleWriterActor());
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");         

            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, "validationActor");

            Props consoleReaderProps = Props.Create(() => new ConsoleReaderActor(validationActor));
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

			consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            MyActorSystem.AwaitTermination();
        }
    }
    #endregion
}
