using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prj_GestionPDC_OASG.Code
{
    public class ListaPaginada<T> : List<T>
    {
		public int IndicePagina { get; private set; }
		public int TotalPaginas { get; private set; }

		public ListaPaginada(List<T> items, int count, int pageIndex, int pageSize)
		{
			IndicePagina = pageIndex;
			TotalPaginas = (int)Math.Ceiling(count / (double)pageSize);
			this.AddRange(items);
		}
		public bool TienePaginaAnterior
		{
			get
			{
				return(IndicePagina > 1);
			}
		}
		public bool TienePaginaSiguiente
		{
			get
			{
				return (IndicePagina < TotalPaginas);
			}
		}

			public static async Task<ListaPaginada<T>> CreateAsync(
				IQueryable<T> source, int pageIndex, int pageSize)
		{
			var count = await source.CountAsync();
			var items = await source.Skip(
				(pageIndex - 1) * pageSize)
				.Take(pageSize).ToListAsync();
			return new ListaPaginada<T>(items, count, pageIndex, pageSize);

		}

	}
}
