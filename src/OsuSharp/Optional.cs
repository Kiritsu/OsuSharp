using System;
using System.Collections.Generic;

namespace OsuSharp
{
    public readonly struct Optional<T> : IEquatable<Optional<T>>, IEquatable<T>
    {
        private static readonly Optional<T> Default = new Optional<T>();
        
        private readonly T _value;
        
        /// <summary>
        ///     Gets whether this struct has a value.
        /// </summary>
        public bool HasValue { get; }

        /// <summary>
        ///     Gets the value of this struct.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     Exception thrown when the struct doesn't hold any value.
        /// </exception>
        public T Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }
                
                throw new InvalidOperationException("The current object has no value.");
            }
        }

        /// <summary>
        ///     Creates a new <see cref="Optional{T}"/> with the given value.
        /// </summary>
        /// <param name="value">Value held by the <see cref="Optional{T}"/></param>
        public Optional(T value)
        {
            _value = value;
            HasValue = true;
        }

        /// <summary>
        ///     Gets the value or return the default value of <see cref="T"/>.
        /// </summary>
        /// <returns>
        ///     Returns the value or return the default value of <see cref="T"/>.
        /// </returns>
        public T OrDefault()
        {
            return _value;
        }
        
        /// <summary>
        ///     Gets the value or return the given value of <see cref="T"/>.
        /// </summary>
        /// <param name="other">
        ///     Value of <see cref="T"/> when this <see cref="Optional{T}"/> doesn't hold any value.
        /// </param>
        /// <returns>
        ///     Returns the value or return the given value of <see cref="T"/>.
        /// </returns>
        public T OrElse(T other)
        {
            return HasValue ? _value : other;
        }

        /// <summary>
        ///     Gets the value or return the given value of <see cref="T"/>.
        /// </summary>
        /// <param name="exception">
        ///     Exception to throw when this <see cref="Optional{T}"/> doesn't hold any value. 
        /// </param>
        /// <returns>
        ///     Returns the value or return the given value of <see cref="T"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Exception thrown when the exception is null.
        /// </exception>
        public T OrThrow(Func<Exception> exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            return HasValue ? _value : throw exception();
        }

        /// <summary>
        ///     Executes the <see cref="function"/> if a value is held by this <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="function">
        ///     Function to execute when this <see cref="Optional{T}"/> holds a value. 
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Exception thrown when the function is null.
        /// </exception>
        public void IfPresent(Action<T> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            if (!HasValue)
            {
                return;
            }
            
            function(_value);
        }

        /// <summary>
        ///     Gets the value or return the value of <see cref="T"/> from the supplier.
        /// </summary>
        /// <param name="supplier">
        ///     Supplier of <see cref="T"/> when this <see cref="Optional{T}"/> doesn't hold any value.
        /// </param>
        /// <returns>
        ///     Returns the value or return the value of <see cref="T"/> from the supplier.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Exception thrown when the supplier is null.
        /// </exception>
        public T OrElseGet(Func<T> supplier)
        {
            if (supplier is null)
            {
                throw new ArgumentNullException(nameof(supplier));
            }
            
            return HasValue ? _value : supplier();
        }

        /// <summary>
        ///     Gets the value if the predicate matches or returns the default value of this <see cref="Optional{T}"/>. 
        /// </summary>
        /// <param name="predicate">
        ///     Condition to return the value held by the <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns the value if the predicate matches or returns the default value of this <see cref="Optional{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Exception thrown when the predicate is null.
        /// </exception>
        public Optional<T> Filter(Func<T, bool> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return HasValue && predicate(_value) ? this : Default;
        }

        /// <summary>
        ///     Maps the value if present and returns an <see cref="Optional{TValue}"/> from the <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="mapper">
        ///     Function that maps the <see cref="T"/> to a <see cref="TValue"/>.
        /// </param>
        /// <typeparam name="TValue">
        ///     Type to map the current <see cref="Optional{T}"/> to.
        /// </typeparam>
        /// <returns>
        ///     Returns the mapped the value if present and returns an <see cref="Optional{TValue}"/> from the <see cref="Optional{T}"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public Optional<TValue> Map<TValue>(Func<T, TValue> mapper)
        {
            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (!HasValue)
            {
                return Optional<TValue>.Default;
            }

            return Optional<TValue>.FromValue(mapper(_value));
        }

        /// <summary>
        ///     Gets whether this <see cref="Optional{T}"/> is equal to the given <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="other">
        ///     Other <see cref="Optional{T}"/> to be compared with the current <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns whether this <see cref="Optional{T}"/> is equal to the given <see cref="Optional{T}"/>.
        /// </returns>
        public bool Equals(Optional<T> other)
        {
            if (!HasValue && !other.HasValue)
            {
                return true;
            }

            if (HasValue != other.HasValue)
            {
                return false;
            }
            
            return EqualityComparer<T>.Default.Equals(_value, other._value);
        }
        
        /// <summary>
        ///     Gets whether this <see cref="T"/> is equal to the given <see cref="T"/>.
        /// </summary>
        /// <param name="other">
        ///     Other <see cref="T"/> to be compared with the current <see cref="T"/>.
        /// </param>
        /// <returns>
        ///     Returns whether this <see cref="T"/> is equal to the given <see cref="T"/>.
        /// </returns>
        public bool Equals(T other)
        {
            return HasValue && Value.Equals(other);
        }

        /// <summary>
        ///     Gets whether this <see cref="Optional{T}"/> is equal to the given <see cref="object"/>.
        /// </summary>
        /// <param name="obj">
        ///     Other <see cref="object"/> to be compared with the current <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns whether this <see cref="Optional{T}"/> is equal to the given <see cref="object"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is T value)
            {
                return Equals(value);
            }

            if (obj is Optional<T> optValue)
            {
                return Equals(optValue);
            }

            return false;
        }

        /// <summary>
        ///     Gets the hashcode for this <see cref="Optional{T}"/>.
        /// </summary>
        /// <returns>
        ///     Returns the hashcode for this <see cref="Optional{T}"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(_value, HasValue);
        }

        /// <summary>
        ///     Gets the <see cref="string"/> representing this <see cref="Optional{T}"/>.
        /// </summary>
        /// <returns>
        ///     Returns the <see cref="string"/> representing this <see cref="Optional{T}"/>.
        /// </returns>
        public override string ToString()
        {
            return HasValue ? Value.ToString() : null;
        }
        
        /// <summary>
        ///     Gets an <see cref="Optional{T}"/> with no value.
        /// </summary>
        /// <returns>
        ///     Returns an <see cref="Optional{T}"/> with no value.
        /// </returns>
        public static Optional<T> FromEmpty()
        {
            return Default;
        }

        /// <summary>
        ///     Gets an <see cref="Optional{T}"/> from the given value.
        /// </summary>
        /// <param name="value">
        ///     Value to be held by the <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns an <see cref="Optional{T}"/> from the given value.
        /// </returns>
        public static Optional<T> FromValue(T value)
        {
            return new Optional<T>(value);
        }
        
        /// <summary>
        ///     Gets whether the left <see cref="T"/> is equal to the right <see cref="T"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="T"/> to be compared with the right operand <see cref="T"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="T"/> to be compared with the left operand <see cref="T"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="T"/> is equal to the right <see cref="T"/>.
        /// </returns>
        public static bool operator ==(Optional<T> left, Optional<T> right)
            => left.Equals(right);

        /// <summary>
        ///     Gets whether the left <see cref="T"/> is not equal to the right <see cref="T"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="T"/> to be compared with the right operand <see cref="T"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="T"/> to be compared with the left operand <see cref="T"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="T"/> is not equal to the right <see cref="T"/>.
        /// </returns>
        public static bool operator !=(Optional<T> left, Optional<T> right) 
            => !(left == right);

        /// <summary>
        ///     Gets whether the left <see cref="Optional{T}"/> is equal to the right <see cref="T"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="Optional{T}"/> to be compared with the right operand <see cref="T"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="Optional{T}"/> to be compared with the left operand <see cref="T"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="Optional{T}"/> is equal to the right <see cref="T"/>.
        /// </returns>        
        public static bool operator ==(Optional<T> left, T right)
            => left.Equals(right);

        /// <summary>
        ///     Gets whether the left <see cref="Optional{T}"/> is not equal to the right <see cref="T"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="Optional{T}"/> to be compared with the right operand <see cref="T"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="Optional{T}"/> to be compared with the left operand <see cref="T"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="Optional{T}"/> is not equal to the right <see cref="T"/>.
        /// </returns>
        public static bool operator !=(Optional<T> left, T right)
            => !(left == right);

        /// <summary>
        ///     Gets whether the left <see cref="T"/> is equal to the right <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="T"/> to be compared with the right operand <see cref="Optional{T}"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="T"/> to be compared with the left operand <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="T"/> is equal to the right <see cref="Optional{T}"/>.
        /// </returns>
        public static bool operator ==(T left, Optional<T> right)
            => right.Equals(left);

        /// <summary>
        ///     Gets whether the left <see cref="T"/> is not equal to the right <see cref="Optional{T}"/>.
        /// </summary>
        /// <param name="left">
        ///     Left operand <see cref="T"/> to be compared with the right operand <see cref="Optional{T}"/>.
        /// </param>
        /// <param name="right">
        ///     Right operand <see cref="T"/> to be compared with the left operand <see cref="Optional{T}"/>.
        /// </param>
        /// <returns>
        ///     Returns whether the left <see cref="T"/> is not equal to the right <see cref="Optional{T}"/>.
        /// </returns>
        public static bool operator !=(T left, Optional<T> right)
            => !(left == right);
    }
}