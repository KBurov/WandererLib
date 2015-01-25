using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Functional.Monads
{
    /// <summary>
    /// 
    /// </summary>
    [DebuggerStepThrough]
    public static class Maybe
    {
        internal const string FuncArgumentErrorMessage = "func cannot be null";
        internal const string NoneFuncArgumentErrorMessage = "none cannot be null";
        internal const string SomeFuncArgumentErrorMessage = "some cannot be null";

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maybe<TValue> From<TValue>(TValue value) where TValue : class
        {
            return new Maybe<TValue>(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="o"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> func) where TInput : class where TResult : class
        {
            Contract.Requires<ArgumentNullException>(func != null, FuncArgumentErrorMessage);

            return o == null ? null : func(o);
        }

        #region Match...
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="o"></param>
        /// <param name="none"></param>
        /// <param name="some"></param>
        /// <returns></returns>
        public static TResult Math<TInput, TResult>(this TInput o, Func<TResult> none, Func<TInput, TResult> some) where TInput : class
        {
            Contract.Requires<ArgumentNullException>(none != null, NoneFuncArgumentErrorMessage);
            Contract.Requires<ArgumentNullException>(some != null, SomeFuncArgumentErrorMessage);

            return o == null ? none() : some(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="o"></param>
        /// <param name="noneValue"></param>
        /// <param name="some"></param>
        /// <returns></returns>
        public static TResult Math<TInput, TResult>(this TInput o, TResult noneValue, Func<TInput, TResult> some) where TInput : class
        {
            Contract.Requires<ArgumentNullException>(some != null, SomeFuncArgumentErrorMessage);

            return o == null ? noneValue : some(o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="o"></param>
        /// <param name="none"></param>
        /// <param name="some"></param>
        public static void Match<TInput>(this TInput o, Action none, Action<TInput> some) where TInput : class
        {
            if (o == null) {
                none();
            }
            else {
                some(o);
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool ReturnSuccess<TInput>(this TInput o) where TInput : class
        {
            return o != null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    [DebuggerStepThrough]
    public struct Maybe<TValue> where TValue : class
    {
        private readonly TValue _value;
        private readonly bool _isSome;

        /// <summary>
        /// Empty value.
        /// </summary>
        public static readonly Maybe<TValue> None = new Maybe<TValue>();

        /// <summary>
        /// 
        /// </summary>
        public bool IsSome { get { return _isSome; } }

        /// <summary>
        /// 
        /// </summary>
        public bool IsNone { get { return !_isSome; } }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="value"></param>
        public Maybe(TValue value)
        {
            _value = value;
            _isSome = value != null;
        }

        #region With...
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public Maybe<TResult> With<TResult>(Func<TValue, TResult> func) where TResult : class
        {
            Contract.Requires<ArgumentNullException>(func != null, Maybe.FuncArgumentErrorMessage);

            return IsSome ? Maybe.From(func(_value)) : Maybe<TResult>.None;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public Maybe<TResult> With<TResult>(Func<TValue, Maybe<TResult>> func) where TResult : class
        {
            Contract.Requires<ArgumentNullException>(func != null, Maybe.FuncArgumentErrorMessage);

            return IsSome ? func(_value) : Maybe<TResult>.None;
        }
        #endregion

        #region Match...
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="none"></param>
        /// <param name="some"></param>
        /// <returns></returns>
        public TResult Match<TResult>(Func<TResult> none, Func<TValue, TResult> some)
        {
            Contract.Requires<ArgumentNullException>(none != null, Maybe.NoneFuncArgumentErrorMessage);
            Contract.Requires<ArgumentNullException>(some != null, Maybe.SomeFuncArgumentErrorMessage);

            return IsSome ? some(_value) : none();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="noneValue"></param>
        /// <param name="some"></param>
        /// <returns></returns>
        public TResult Match<TResult>(TResult noneValue, Func<TValue, TResult> some)
        {
            Contract.Requires<ArgumentNullException>(some != null, Maybe.SomeFuncArgumentErrorMessage);

            return IsSome ? some(_value) : noneValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="none"></param>
        /// <param name="some"></param>
        public void Match(Action none, Action<TValue> some)
        {
            Contract.Requires<ArgumentNullException>(none != null, Maybe.NoneFuncArgumentErrorMessage);
            Contract.Requires<ArgumentNullException>(some != null, Maybe.SomeFuncArgumentErrorMessage);

            if (IsSome) {
                some(_value);
            }
            else {
                none();
            }
        }
        #endregion
    }
}