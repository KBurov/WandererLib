using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Functional.Monads
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerStepThrough]
    public struct Maybe
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<T> From<T>(T value)
        {
            return new Maybe<T>(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DebuggerStepThrough]
    public struct Maybe<T>
    {
        private readonly T _value;
        private readonly bool _isSome;

        /// <summary>
        /// 
        /// </summary>
        public static readonly Maybe<T> None = new Maybe<T>();

        /// <summary>
        /// 
        /// </summary>
        public bool IsSome { get { return _isSome; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNone { get { return !_isSome; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public Maybe(T value)
        {
            _value = value;
            _isSome = value != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public Maybe<A> Bind<A>(Func<T, Maybe<A>> func)
        {
            Contract.Requires<ArgumentNullException>(func != null, "func cannot be null");

            return IsSome ? func(_value) : Maybe<A>.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="A"></typeparam>
        /// <param name="none"></param>
        /// <param name="some"></param>
        /// <returns></returns>
        public A Match<A>(Func<A> none, Func<T, A> some)
        {
            Contract.Requires<ArgumentNullException>(none != null, "none cannot be null");
            Contract.Requires<ArgumentNullException>(some != null, "some cannot be null");

            return IsSome ? some(_value) : none();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="none"></param>
        /// <param name="some"></param>
        public void Match(Action none, Action<T> some)
        {
            Contract.Requires<ArgumentNullException>(none != null, "none cannot be null");
            Contract.Requires<ArgumentNullException>(some != null, "some cannot be null");

            if (IsSome) {
                some(_value);
            }
            else {
                none();
            }
        }
    }
}