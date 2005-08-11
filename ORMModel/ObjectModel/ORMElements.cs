using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Modeling;
using System.ComponentModel;

namespace Neumont.Tools.ORM.ObjectModel
{
	public abstract partial class ORMModelElement : IORMExtendableElement
	{
		/// <summary>See <see cref="ModelElement.GetDisplayProperties"/></summary>
		public override PropertyDescriptorCollection GetDisplayProperties(ModelElement requestor, ref PropertyDescriptor defaultPropertyDescriptor)
		{
			return ExtendableElementUtility.GetProperties(this, base.GetDisplayProperties(requestor, ref defaultPropertyDescriptor));
		}
	}

	public abstract partial class ORMNamedElement : IORMExtendableElement
	{
		/// <summary>See <see cref="ModelElement.GetDisplayProperties"/></summary>
		public override PropertyDescriptorCollection GetDisplayProperties(ModelElement requestor, ref PropertyDescriptor defaultPropertyDescriptor)
		{
			return ExtendableElementUtility.GetProperties(this, base.GetDisplayProperties(requestor, ref defaultPropertyDescriptor));
		}
	}
	/// <summary>
	/// Utility methods for building extension objects
	/// </summary>
	public static class ExtendableElementUtility
	{
		/// <summary>
		/// Merge properties from an extenable object with
		/// properties from its associated extension elements
		/// </summary>
		/// <param name="extendableElement">The extendable element</param>
		/// <param name="baseProperties">The original properties from the extendable element</param>
		/// <returns>A merged PropertyDescriptorCollection</returns>
		public static PropertyDescriptorCollection GetProperties(IORMExtendableElement extendableElement, PropertyDescriptorCollection baseProperties)
		{
			foreach (IORMPropertyExtension extension in extendableElement.ExtensionCollection)
			{
				if (0 != (extension.ExtensionPropertySettings & ORMExtensionPropertySettings.MergeAsChildProperties))
				{

				}
				if (0 != (extension.ExtensionPropertySettings & ORMExtensionPropertySettings.MergeAsDirectProperty))
				{
					ModelElement element = extension as ModelElement;
					PropertyDescriptorCollection collection = element.GetProperties();
					foreach (PropertyDescriptor descriptor in collection)
					{
						baseProperties.Add(descriptor);
					}
				}
			}
			return baseProperties;
		}
	}
}