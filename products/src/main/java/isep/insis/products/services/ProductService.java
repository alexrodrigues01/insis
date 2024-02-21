package isep.insis.products.services;

import isep.insis.products.model.Product;
import isep.insis.products.model.ProductDTO;
import isep.insis.products.model.ProductDetailDTO;

import java.util.Optional;

public interface ProductService {
    Optional<ProductDTO> findBySku(final String sku);

    Optional<Product> getProductBySku(final String sku );

    Iterable<ProductDTO> findByDesignation(final String designation);

    Iterable<ProductDTO> getCatalog();

    ProductDetailDTO getDetails(final String sku);

    ProductDTO create(final Product manager);

    ProductDTO updateBySku(final String sku, final Product product);

    void deleteBySku(final String sku);
}
