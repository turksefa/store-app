using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repositories.EFCore
{
	public static class BookRepositoryExtensions
	{
		public static IQueryable<Book> FilterBooks(this IQueryable<Book> books, uint MinPrice, uint MaxPrice)
		{
			return books.Where(b => b.Price > MinPrice && b.Price <= MaxPrice);
		}

		public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
		{
			if (!string.IsNullOrWhiteSpace(searchTerm))
				return books;
			var lowerCaseTerm = searchTerm.Trim().ToLower();
			return books.Where(b => b.Title
			.ToLower()
			.Contains(lowerCaseTerm));
		}

		public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
		{
			if (string.IsNullOrWhiteSpace(orderByQueryString))
				return books;

			var orderParams = orderByQueryString.Trim().Split(',');
			var propertyInfos = typeof(Book).GetProperties();
			var orderQueryBuilder = new StringBuilder();

			foreach ( var param in orderParams ) { 
				if(string.IsNullOrWhiteSpace(param))
					continue;

				var direction = param.EndsWith(" desc") ? "descending" : "ascending";
				orderQueryBuilder.Append($"{propertyInfos.FirstOrDefault()?.Name} {direction},");
			}
			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',',' ');
			return books.OrderBy(orderQuery);
		}
	}
}
