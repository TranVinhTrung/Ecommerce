using Ecommerce.Application.DTOs;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Core.Entities;
using Ecommerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }

        public async Task<PagedResultDto<ProductResponseDto>> GetAllAsync(ProductQueryDto query)
        {
            var productsQuery =  _repository.GetQuery();

            // Search theo tên Product
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                productsQuery = productsQuery
                    .Where(p => p.Name.Contains(query.Search));
            }

            // Filter theo Category
            if (query.CategoryId.HasValue)
            {
                productsQuery = productsQuery
                    .Where(p => p.CategoryId == query.CategoryId.Value);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if(query.SortBy == "price"){
                    productsQuery = productsQuery
                        .OrderBy(p => p.Price);
                }
                else if (query.SortBy == "price_desc")
                {
                    productsQuery = productsQuery
                        .OrderByDescending(p => p.Price);
                }
                else if (query.SortBy == "name")
                {
                    productsQuery = productsQuery
                        .OrderBy(p => p.Name);
                }
                else if (query.SortBy == "name_desc")
                {
                    productsQuery = productsQuery
                        .OrderByDescending(p => p.Name);
                }
            }

            var totalCount = await productsQuery.CountAsync();

            var totalPages = (int)Math.Ceiling(
                (double)totalCount / query.PageSize);

            //Pagination
            /* Công thức Skip = (Page - 1) × PageSize Take = PageSize Skip(): bỏ qua bao nhiêu record.
                Take(): lấy bao nhiêu record.
                Pagination = (Page - 1) × PageSize + Take(PageSize).
             */
            var skip = (query.Page - 1) * query.PageSize;
            productsQuery = productsQuery
                .Skip(skip)
                .Take(query.PageSize);

            //Thực thi
            var products = await productsQuery.ToListAsync();

            // Mapping
            var items = products.Select(product => new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            });

            // Kết quả trả về API
            return new PagedResultDto<ProductResponseDto>
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = query.Page,
                PageSize = query.PageSize,
                Items = items
            };
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
             var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name
            };
        }

        public async Task<ProductResponseDto> CreateAsync(ProductCreateDto dto)
        {

            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
                throw new BusinessException("Category does not exist.");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now
            };


            var created = await _repository.AddAsync(product);
            var category = await _categoryRepository.GetByIdAsync(created.CategoryId);

            return new ProductResponseDto
            {
                Id = created.Id,
                Name = created.Name,
                Description = created.Description,
                Price = created.Price,
                Stock = created.Stock,
                CategoryId = created.CategoryId,
                CategoryName = category?.Name ?? string.Empty
            };
        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(dto.CategoryId);

            if (!categoryExists)
                throw new BusinessException("Category does not exist.");


            var product = new Product
            {
                Id = id,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            return await _repository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {   
            return await _repository.DeleteAsync(id);
        }
    }
}
