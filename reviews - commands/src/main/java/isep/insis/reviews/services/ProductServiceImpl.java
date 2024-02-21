package isep.insis.reviews.services;


import isep.insis.reviews.model.Product;
import isep.insis.reviews.model.ProductDTO;
import isep.insis.reviews.rabbitmq.Runner;
import isep.insis.reviews.repositories.ProductRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class ProductServiceImpl implements ProductService {

    @Autowired
    private ProductRepository repository;

    @Override
    public ProductDTO create(final Product product) {
        final Product p = new Product(product.getSku());
        return repository.save(p).toDto();
    }
    @Override
    public void deleteBySku(String sku) {
        repository.deleteBySku(sku);
    }
}
