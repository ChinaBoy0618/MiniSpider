using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MiniIOC.Framwork.Config
{
    public class MiniIOCSection : ConfigurationSection
    {
        public MiniIOCSection()
        { 
        
        }

        [ConfigurationProperty("XmlCollectionPath", IsRequired = false)]
        public string XmlCollectionPath
        {
            get {
                return this["XmlCollectionPath"] != null ? this["XmlCollectionPath"].ToString() : "";
            }
            set { this["XmlCollectionPath"] = value; }
        }
        [ConfigurationProperty("AssemblyPath", IsRequired = false)]
        public string AssemblyPath
        {
            get
            {
                return this["AssemblyPath"] != null ? this["AssemblyPath"].ToString() : "";
            }
            set { this["AssemblyPath"] = value; }
        }
        [ConfigurationProperty("MiniIOCEntitys", IsRequired = true)]
        [ConfigurationCollection(typeof(MiniIOCEntitysCollection), AddItemName = "entity")]
        public MiniIOCEntitysCollection MiniIOCEntitys
        {
            get { return (MiniIOCEntitysCollection)this["MiniIOCEntitys"]; }
            set { this["MiniIOCEntitys"] = value; }
        }
       

    }
    public class MiniIOCEntitysCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EntityElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EntityElement)element).Type;
        }
    }
    public class EntityElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get
            {
                return this["type"].ToString();
            }
            set { this["type"] = value; }
        }
        [ConfigurationProperty("mapto", IsRequired = true)]
        public string MapTo
        {
            get
            {
                return this["mapto"].ToString(); 
            }
            set { this["mapto"] = value; }
        }
    }
}
