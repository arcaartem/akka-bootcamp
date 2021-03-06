using System;
using Akka.Actor;

namespace WinTail
{
    class ConsoleReaderActor : UntypedActor
    {
        public const string ExitCommand = "exit";
		public const string StartCommand = "start";
			
        private IActorRef _validationActor;

        public ConsoleReaderActor(IActorRef validationActor)
        {
            _validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
			if (message.Equals (StartCommand))
            {
				DoPrintInstructions ();
			}

			GetAndValidateInput();
        }

		#region Internal methods
		private void DoPrintInstructions()
		{
			Console.WriteLine("Write whatever you want into the console!");
			Console.WriteLine("Some entries will pass validation, and some won't...\n\n");
			Console.WriteLine("Type 'exit' to quit this application at any time.\n");
		}

		private void GetAndValidateInput()
		{
			var message = Console.ReadLine();

            if (!string.IsNullOrEmpty(message) && String.Equals(message, ExitCommand, StringComparison.OrdinalIgnoreCase))
            {
                Context.System.Shutdown();
                return;
            }

            _validationActor.Tell(message);
		}
		#endregion
    }
}