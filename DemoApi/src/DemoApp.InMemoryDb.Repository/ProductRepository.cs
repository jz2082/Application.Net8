using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using InMemoryDb.Model;
using InMemoryDb.DataEntity;
using Application.Framework;

namespace Demo.InMemoryDb.Repository;

public class ProductRepository(InMemoryDbContext context, ILogger<ProductRepository> logger) : BaseRepository<Product, Guid>(logger), IProductRepository
{
    private readonly InMemoryDbContext _context = context;

    private static Product? EntityToModel(ProductEntity? x)
    {
        if (x == null) return null;
        return new Product
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            ProductCode = x.ProductCode,
            TagList = x.TagList,
            ReleaseDate = x.ReleaseDate,
            Price = x.Price,
            Description = x.Description,
            StarRating = x.StarRating,
            ImageUrl = x.ImageUrl
        };
    }

    private static ProductEntity? ModelToEntity(Product? x)
    {
        if (x == null) return null;
        return new ProductEntity
        {
            Id = x.Id,
            ProductId = x.ProductId,
            ProductName = x.ProductName,
            ProductCode = x.ProductCode,
            TagList = x.TagList,
            ReleaseDate = x.ReleaseDate,
            Price = x.Price,
            Description = x.Description,
            StarRating = x.StarRating,
            ImageUrl = x.ImageUrl
        };
    }

    private static void ModelToEntity(Product dto, ProductEntity? x)
    {
        if (dto != null && x != null)
        {
            x.Id = dto.Id;
            x.ProductId = dto.ProductId;
            x.ProductName = dto.ProductName;
            x.ProductCode = dto.ProductCode;
            x.TagList = dto.TagList;
            x.ReleaseDate = dto.ReleaseDate;
            x.Price = dto.Price;
            x.Description = dto.Description;
            x.StarRating = dto.StarRating;
            x.ImageUrl = dto.ImageUrl;
        }
    }

    #region Validator 

    protected override void SetupValidatorList()
    {
        InsertValidatorList = new List<Func<Product, Task<AdditionalInfo>>>()
        {
            ValidateNullEntityAsync,
            ValidateUniqueEntityAsync,
            ValidateMandatoryAttribueAsync,
            ValidateRangeAttribueAsync
        };
        UpdateValidatorList = new List<Func<Product, Task<AdditionalInfo>>>()
        {
            ValidateNullEntityAsync,
            ValidateExistEntityAsync,
            ValidateMandatoryAttribueAsync,
            ValidateRangeAttribueAsync,
            ValidateConcurrencyEntityAsync
        };
        DeleteValidatorList = new List<Func<Product, Task<AdditionalInfo>>>()
        {
            ValidateNullEntityAsync,
            ValidateExistEntityAsync
        };
    }

    private async Task<AdditionalInfo> ValidateMandatoryAttribueAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            if (string.IsNullOrEmpty(entity.ProductName))
            {
                errInfo.AddError("ProductName", "Product Name is required.");
            }
            if (string.IsNullOrEmpty(entity.ProductCode))
            {
                errInfo.AddError("ProductCode", "Product Code is required.");
            }
            if (entity.Price <= 0)
            {
                errInfo.AddError("Price", "Price is required.");
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                errInfo.AddError("Description", "Description is required.");
            }
            if (entity.StarRating <= 0)
            {
                errInfo.AddError("StarRating", "Star Rating is required.");
            }
        }
        return await Task.FromResult(errInfo);
    }

    private async Task<AdditionalInfo> ValidateRangeAttribueAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            if (entity.StarRating <= 0 || entity.StarRating > 5)
            {
                errInfo.AddError("StarRating", "Star Rating should between 1 ~ 5.");
            }
        }
        return await Task.FromResult(errInfo);
    }

    private async Task<AdditionalInfo> ValidateNullEntityAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity == null)
        {
            errInfo.AddError("Product", "Product can not be null.");
        }
        return await Task.FromResult(errInfo);
    }

    private async Task<AdditionalInfo> ValidateUniqueEntityAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == entity.ProductId)
                .ConfigureAwait(false);
            if (originalEntity != null)
            {
                errInfo.AddError("Product", "Product already exist and can not be added.");
            }
        }
        return await Task.FromResult(errInfo);
    }

    private async Task<AdditionalInfo> ValidateExistEntityAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == entity.ProductId)
                .ConfigureAwait(false);
            if (originalEntity == null)
            {
                errInfo.AddError("Product", "Product is not found and can not be updated/deleted.");
            }
        }
        return await Task.FromResult(errInfo);
    }

    private async Task<AdditionalInfo> ValidateConcurrencyEntityAsync(Product entity)
    {
        var errInfo = new AdditionalInfo();

        if (entity != null)
        {
            var originalEntity = await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entity.Id)
                .ConfigureAwait(false);
            if (originalEntity != null && !originalEntity.RowVersion.SequenceEqual(entity.RowVersion))
            {
                errInfo.AddError("Product", "Data has been modified since entitiy were loaded.");
            }
        }
        return await Task.FromResult(errInfo);
    }

    #endregion

    public async Task<IEnumerable<Product>?> GetAllAsync()
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "ProductRepository");
        info.Add("Method", "GetAllAsync");
        info.Add("Message", $"ProductRepository.GetAllAsync()");
        info.Add("ReturnType", "IEnumerable<Product>");
        IEnumerable<Product> returnValue = [];

        try
        {
            ClearViolation();
            returnValue = await _context
                .Products
                .Select(x => new Product
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    ProductCode = x.ProductCode,
                    TagList = x.TagList,
                    ReleaseDate = x.ReleaseDate,
                    Price = x.Price,
                    Description = x.Description,
                    StarRating = x.StarRating,
                    ImageUrl = x.ImageUrl
                })
                .ToListAsync()
                .ConfigureAwait(false);
            if (returnValue == null)
            {
                RuleViolation.Add("returnValue", "Product list not found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<Product?> GetAsync(int entityId)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "ProductRepository");
        info.Add("Method", "GetAsync");
        info.Add("Message", $"ProductRepository.GetAsync(int entityId: {entityId})");
        info.Add("ReturnType", "Product?");
        Product? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context
                .Products
                .SingleOrDefaultAsync(x => x.ProductId == entityId)
                .ConfigureAwait(false);
            returnValue = EntityToModel(entity);
            if (entity == null)
            {
                RuleViolation.Add("returnValue", $"Product with Id: {entityId} not found.");
            }
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<Product?> AddAsync(Product dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "ProductRepository");
        info.Add("Method", "AddAsync");
        info.Add("Message", $"ProductRepository.AddAsync(Product dto)");
        info.Add("ReturnType", "Product?");
        Product? returnValue;

        try
        {
            ClearViolation();
            var entity = (dto == null) ? throw new ArgumentException("Add Product with null.") : ModelToEntity(dto);
            if (entity != null)
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            returnValue = EntityToModel(entity);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<Product?> UpdateAsync(Product dto)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "ProductRepository");
        info.Add("Method", "UpdateAsync");
        info.Add("Message", $"ProductRepository.UpdateAsync(Product dto)");
        info.Add("ReturnType", "Product?");
        Product? returnValue;

        try
        {
            ClearViolation();
            var entity = await _context.Products.FindAsync(dto.Id) ?? throw new ArgumentException(
                $"Trying to update product: entity with ID {dto.Id} not found."
            );
            ModelToEntity(dto, entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            returnValue = EntityToModel(entity);
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }

    public async Task<bool> DeleteAsync(int productId)
    {
        var info = new AdditionalInfo();
        info.Add(AppLogger.Application, "Demo.InMemoryDb.Repository");
        info.Add(AppLogger.Module, "ProductRepository");
        info.Add("Method", "DeleteAsync");
        info.Add("Message", $"ProductRepository.DeleteAsync(int productId)");
        bool returnValue = false;

        try
        {
            ClearViolation();
            var entity = await _context
                .Products
                .SingleOrDefaultAsync(x => x.ProductId == productId)
                .ConfigureAwait(false) ?? throw new ArgumentException(
                    $"Trying to delete product: entity with ID {productId} not found."
                );
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            returnValue = true;
        }
        catch (Exception ex)
        {
            ExceptionMessage = ex.Message;
            info.SetExceptionData(AdditionalInfoType.TotalMilliseconds, ex);
            throw;
        }
        return returnValue;
    }
}
