﻿using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System;
using System.Collections.Generic;

namespace BL
{
	public class ContentPlayer : IContentPlayer
	{
		private readonly ICategoryRepository categoryRepository;
		private readonly IContentRepository contentRepository;

		public ContentPlayer(IPlaylistRepository playlistRepository, 
			ICategoryRepository categoryRepository, 
			IContentRepository contentRepository)
		{
			this.categoryRepository = categoryRepository;
			this.contentRepository = contentRepository;
		}

		public IEnumerable<Content> GetContents()
		{
			return this.contentRepository.GetAll();
		}
	}
}
