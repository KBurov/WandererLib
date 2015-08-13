namespace Wanderer.Library.WindowsApi.Helpers.ObjectPicker
{
    /// <summary>
    /// Details of a directory object selected in the <see cref="ObjectPickerDialog"/>.
    /// </summary>
    public sealed class DirectoryObject
    {
        /// <summary>
        /// Gets the Active Directory path for this directory object.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the name of the schema class for this directory object (objectClass attribute).
        /// </summary>
        public string SchemaClassName { get; }

        /// <summary>
        /// Gets the directory object's relative distinguished name (RDN).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the objects user principal name (userPrincipalName attribute).
        /// </summary>
        public string Upn { get; }

        /// <summary>
        /// Gets attributes retrieved by the object picker as it makes the selection.
        /// </summary>
        public object[] FetchedAttributes { get; }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="name">directory object's relative distinguished name (RDN)</param>
        /// <param name="path">Active Directory path for this directory object</param>
        /// <param name="schemaClass">name of the schema class for this directory object (objectClass attribute)</param>
        /// <param name="upn">objects user principal name (userPrincipalName attribute)</param>
        /// <param name="fetchedAttributes">attributes retrieved by the object picker as it makes the selection</param>
        public DirectoryObject(string name, string path, string schemaClass, string upn, object[] fetchedAttributes)
        {
            Name = name;
            Path = path;
            SchemaClassName = schemaClass;
            Upn = upn;
            FetchedAttributes = fetchedAttributes;
        }
    }
}