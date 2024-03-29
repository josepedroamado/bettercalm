﻿using BLInterfaces;
using Domain;
using ImporterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using BL.Utils;
using System.IO;
using System.Reflection;
using Domain.Exceptions;

namespace BL
{
	public class ImporterLogic : IImporterLogic
	{
		private IContentLogic contentLogic;
		private const string importersFolder = "Importers";
		private const string assemblyTypeFilter = "*.dll";

		public ImporterLogic(IContentLogic contentLogic)
		{
			this.contentLogic = contentLogic;
		}

		public void Import(string type, string filePath)
		{
			IImporter importer = GetImporter(type);
			List<ImporterModel.Content> toImport = importer.Import(filePath).ToList();
			List<Content> contents = toImport.Select(importContent => ContentModelConverter.GetDomainContent(importContent)).ToList();
			contents.ForEach(content => contentLogic.CreateContent(content));
		}

		public List<string> GetTypes()
		{
			List<Type> importers = GetImporters();
			List<string> types = new List<string>();

			foreach(Type importerType in importers)
			{
				IImporter importerInstance = (IImporter)Activator.CreateInstance(importerType);
				string typeId = importerInstance.GetId();
				if (!types.Contains(typeId))
				{
					types.Add(typeId);
				}
			};
			return types;
		}

		private List<Type> GetImporters()
		{
			List<Type> importers = new List<Type>();
			string path = GetImportersFolderFullPath();
			foreach (string assembly in Directory.GetFiles(path, assemblyTypeFilter))
			{
				Assembly loadedAssembly = Assembly.LoadFrom(assembly);
				IEnumerable<Type> types = GetTypesInAssembly<IImporter>(loadedAssembly);
				importers.AddRange(types);
			}
			return importers;
		}

		private IImporter GetImporter(string typeId)
		{
			string path = GetImportersFolderFullPath();
			foreach(string assembly in Directory.GetFiles(path, assemblyTypeFilter))
			{
				Assembly loadedAssembly = Assembly.LoadFrom(assembly);
				IEnumerable<Type> types = GetTypesInAssembly<IImporter>(loadedAssembly);
				foreach(Type type in types)
				{
					IImporter importer = (IImporter)Activator.CreateInstance(type);
					if (importer.GetId().Equals(typeId, StringComparison.OrdinalIgnoreCase))
					{
						return importer;
					}
				}
			}
			throw new ImporterNotFoundException(typeId, path);
		}

		private string GetImportersFolderFullPath()
		{
			string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			return Path.Combine(executingDirectory, importersFolder);
		}

		private IEnumerable<Type> GetTypesInAssembly<IImporter>(Assembly assembly)
		{
			List<Type> types = new List<Type>();
			foreach(Type type in assembly.GetTypes())
			{
				if (typeof(IImporter).IsAssignableFrom(type))
				{
					types.Add(type);
				}
			}
			return types;
		}
	}
}
