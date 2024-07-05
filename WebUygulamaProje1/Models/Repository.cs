﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebUygulamaProje1.Utility;

namespace WebUygulamaProje1.Models
{
	public class Repository<T> : IRepository<T> where T : class
	{

		private readonly UygulamaDbContext _uygulamaDbContext;
		internal DbSet<T> dbSet;

		public Repository(UygulamaDbContext uygulamaDbContext)
		{
			_uygulamaDbContext = uygulamaDbContext;
			this.dbSet = _uygulamaDbContext.Set<T>();
			_uygulamaDbContext.Kitaplar.Include(k => k.KitapTuru).Include(k => k.KitapTuruId);
		}
		public void Ekle(T entitiy)
		{
			dbSet.Add(entitiy);
		}

		public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
		{
			IQueryable<T> sorgu = dbSet;
			sorgu = sorgu.Where(filtre);

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					sorgu = sorgu.Include(includeProp);
				}
			}

			return sorgu.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProps= null)
		{
			IQueryable<T> sorgu = dbSet;

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach(var includeProp in includeProps.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) { 
				sorgu=sorgu.Include(includeProp);
				}
			}

			return sorgu.ToList();
		}

		public void Sil(T entitiy)
		{
			dbSet.Remove(entitiy);
		}

		public void SilAralik(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities);
		}
	}
}
