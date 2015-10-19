﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Backpack.Features
{
    internal static class Require
    {
        public static void That(bool condition, string message = "")
        {
            That<Exception>(condition, message);
        }

        public static void That<TException>(bool condition, string message = "") where TException : Exception
        {
            if (!condition)
            {
                throw (TException)Activator.CreateInstance(typeof(TException), message);
            }
        }

        public static void Not<TException>(bool condition, string message = "") where TException : Exception
        {
            That<TException>(!condition, message);
        }

        public static void Not(bool condition, string message = "")
        {
            Not<Exception>(condition, message);
        }

        public static void NotNull(object value, string message = "")
        {
            That<NullReferenceException>(value != null, message);
        }

        public static void Equal<T>(T left, T right, string message = "Values must be equal")
        {
            That(left != null && right != null && left.Equals(right), message);
        }

        public static void NotEqual<T>(T left, T right, string message = "Values must not be equal")
        {
            That(left != null && right != null && !left.Equals(right), message);
        }

        public static void Contains<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "")
        {
            That(collection != null && collection.Any(predicate), message);
        }

        public static void Items<T>(IEnumerable<T> collection, Func<T, bool> predicate, string message = "")
        {
            That(collection != null && !collection.Any(x => !predicate(x)));
        }

        public static void NotNullOrEmpty(string value, string message = "String cannot be null or empty")
        {
            That(!string.IsNullOrEmpty(value), message);
        }

        public static class Argument
        {
            public static void Is(bool condition, string message = "")
            {
                That<ArgumentException>(condition, message);
            }

            public static void IsNot(bool condition, string message = "")
            {
                Is(!condition, message);
            }

            public static void NotNull(object value, string paramName = "")
            {
                That<ArgumentNullException>(value != null, paramName);
            }

            public static void NotNullOrEmpty(string value, string paramName = "")
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Parameter value cannot be null or empty", paramName);
                }
            }

            public static void NotEmpty(IEnumerable collection, string paramName = "")
            {
                if (!collection.Cast<object>().Any())
                {
                    throw new ArgumentException("Collection cannot be empty", paramName);
                }
            }
        }
    }
}