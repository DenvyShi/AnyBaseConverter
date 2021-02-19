using System.Collections.Generic;
using System.Linq;

namespace AnyBaseConverter
{

    /// <summary>
    ///     Dictionary with unique key and unique value combination.
    /// </summary>
    /// <typeparam name="TKey">
    ///     First type parameter.
    /// </typeparam>
    /// <typeparam name="TValue">
    ///     Second type parameter.
    /// </typeparam>
    public class UniqueDictionary<TKey, TValue>
    {
        /// <summary>
        ///     Get the underlying Dictionary object.
        /// </summary>
        public Dictionary<TKey, TValue> Dictionary { get; private set; }

        /// <summary>
        ///     Get the underlying Inverse (Reversed Key:Value) Dictionary object.
        /// </summary>
        public Dictionary<TValue, TKey> InverseDictionary { get; private set; }

        /// <summary>
        ///     Initialize a new UniqueDictionary.
        /// </summary>
        public UniqueDictionary()
        {
            Dictionary = new Dictionary<TKey, TValue>();
            InverseDictionary = new Dictionary<TValue, TKey>();
        }

        /// <summary>
        ///     Get dictionary value based on the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key of the requested value.
        /// </param>
        /// <returns>
        ///     Value represented by the specified key.
        /// </returns>
        public TValue GetFromKey(TKey key)
        {
            return Dictionary[key];
        }

        /// <summary>
        ///     Get dictionary key based on the specified value.
        /// </summary>
        /// <param name="value">
        ///     The value of the request key.
        /// </param>
        /// <returns>
        ///     Value represented by the specified key.
        /// </returns>
        public TKey GetFromValue(TValue value)
        {
            return InverseDictionary[value];
        }

        /// <summary>
        ///     Get how many items are stored in the dictionary.
        /// </summary>
        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        /// <summary>
        ///     Add a new item to the dictionary.
        /// </summary>
        /// <param name="key">
        ///     Item key.
        /// </param>
        /// <param name="value">
        ///     Item value.
        /// </param>
        public void Add(TKey key, TValue value)
        {
            Dictionary.Add(key, value);
            InverseDictionary.Add(value, key);
        }

        /// <summary>
        ///     Checks whether dicitonary contains the specified value.
        /// </summary>
        /// <param name="value">
        ///     The item value.
        /// </param>
        /// <returns>
        ///     True if value exists, false otherwise.
        /// </returns>
        public bool ContainsValue(TValue value)
        {
            return InverseDictionary.ContainsKey(value);
        }
    }

    /// <summary>
    ///     Provides global settings for Obfuscation.
    /// </summary>
    public static class Settings
    {
        /// <summary>
        ///     Read-only character dictionary,
        ///     Generated from AllCharacterSet - DummyCharacterSet.
        /// </summary>
        public static readonly UniqueDictionary<int, char> ValidCharacterSet;

        /// <summary>
        ///     Read-only character count dictionary,
        ///     Which denotes the base number to use in number to character conversion.
        /// </summary>
        // public static readonly int Base;

        /// <summary>
        ///     Read-only minimum length of the generated sequence.
        /// </summary>
        public readonly static int MinimumLength = 8;

        /// <summary>
        ///     Read-only character set that represents all the characters which can be used for conversion.
        /// </summary>
        public readonly static char[] AllCharacterSet = BaseConverter.CHARSET62.ToCharArray();

        /*
        public readonly static char[] AllCharacterSet =
        {
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            '0','1','2','3','4','5','6','7','8','9'
        };*/
        /// <summary>
        ///     Read-only character set that represents dummy values.
        /// </summary>
        public readonly static char[] DummyCharacterSet =
        {
            'A','D','M','N','Q','V','Z','c','d','g','j','k','n','q','r','s','u','x','z','2','4','7'
        };

        static Settings()
        {
            /*ValidCharacterSet = new UniqueDictionary<int, char>();

            int index = 0;
            for (int i = 0; i < AllCharacterSet.Length; i++)
            {
                // Ignore dummy characters.
                if (DummyCharacterSet.Contains(AllCharacterSet[i]))
                    continue;

                ValidCharacterSet.Add(index, AllCharacterSet[i]);
                index++;
            }

            // Base = ValidCharacterSet.Count;*/
        }
    }
}
