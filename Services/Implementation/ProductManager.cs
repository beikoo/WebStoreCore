namespace Services.Implementations
{
    using AutoMapper;
    using Data;
    using Models;
    using Services.Common;
    using Services.CustomModels;
    using Services.CustomModels.MapperSettings;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductManager : BaseManager<ProductModel>
    {
        public List<ProductModel> AllProducts
        {
            get
            {
                var res = MapperConfigurator.Mapper.Map<List<ProductModel>>(this.context.Products.ToList());

                return res;
            }
        }
        public ProductManager() : base(new WebStoreDbContext())
        {

        }
        public override string Add(ProductModel model)
        {
            try
            {
                using (context)
                {
                    Product product = MapperConfigurator.Mapper.Map<Product>(model);
                    this.context.Products.Add(product);
                    this.context.SaveChanges();
                    return "";
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public override string Delete(ProductModel model)
        {
            try
            {
                using (context)
                {

                    Product product = MapperConfigurator.Mapper.Map<Product>(model);
                    this.context.Products.Remove(product);
                    int res = this.context.SaveChanges();
                    if (res == 1)
                    {
                        return "";
                    }
                    return string.Format($"{MessageAndVariables.UpdateFail} {model.Name}.");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public override string Update(ProductModel model)
        {
            try
            {
                using (context)
                {
                    Product product = MapperConfigurator.Mapper.Map<Product>(model);
                    this.context.Products.Update(product);
                    int res = this.context.SaveChanges();
                    if (res == 1)
                    {
                        return "";
                    }
                    return string.Format($"{MessageAndVariables.UpdateFail} {model.Name}.");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

