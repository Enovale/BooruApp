using System;
using System.Reactive;
using System.Windows.Input;

namespace BooruApp.Infrastructure
{
    public static class BasicCommand
    {
        public delegate void BasicCommandActionDelegate<in TInput2>(TInput2 parameter);
        
        public static BasicCommand<TInput3, Unit> Create<TInput3>(BasicCommandActionDelegate<TInput3> action)
        {
            return new BasicCommand<TInput3, Unit>(args =>
            {
                action(args);
                return Unit.Default;
            });
        }

        public static BasicCommand<TInput4, TResult2> CreateFromObservable<TInput4, TResult2>(Func<TInput4, TResult2> predicate) // TODO Make actual observable
        {
            return new BasicCommand<TInput4, TResult2>(predicate);
        }
    }
    
    public class BasicCommand<TInput, TOutput> : ICommand //where TOutput : IObservable<TOutput>
    {
        public Func<TInput, TOutput> BoundAction { get; }

        internal BasicCommand(Func<TInput, TOutput> action)
        {
            BoundAction = action;
        }

        public bool CanExecute(object? parameter)
        {
            // TODO I don't fucking care man
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is TInput p)
                BoundAction(p);
        }

        public event EventHandler? CanExecuteChanged;
    }
}