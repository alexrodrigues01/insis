package isep.insis.reviews.services;

import isep.insis.reviews.model.Product;
import isep.insis.reviews.model.ProductDTO;

public interface ProductService {
    ProductDTO create(final Product manager);

    void deleteBySku(final String sku);
}
