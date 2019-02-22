namespace System.ComponentModel
{
    public static class AttributeCollectionExtensions
    {
        /// <summary>
        /// Gets the attribute with the specified type.
        /// if the attribute does not exist, the default value for the attribute type is returned.
        /// </summary>
        /// <typeparam name="T">Searched attribute type.</typeparam>
        /// <param name="attributes">Attribute collection.</param>
        /// <returns></returns>
        public static T Get<T>(this AttributeCollection attributes) where T : Attribute
        {
            return attributes[typeof(T)] as T;
        }
    }
}
