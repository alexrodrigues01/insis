package isep.insis.reviews.bootstrapper;

import isep.insis.reviews.model.Product;
import isep.insis.reviews.model.Review;
import isep.insis.reviews.repositories.ProductRepository;
import isep.insis.reviews.repositories.ReviewRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.stereotype.Component;

import java.time.LocalDate;

@Component
//@Profile("bootstrap")
public class ProductBootstrapper implements CommandLineRunner {

    @Autowired
    private ProductRepository pRepo;

    @Autowired
    private ReviewRepository rRepo;

    @Override
    public void run(String... args) throws Exception {

        if (pRepo.findBySku("asd578fgh267").isEmpty()) {
            Product p1 = new Product("asd578fgh267");
            pRepo.save(p1);
            Review r1 = new Review(1L, 1, "approved", "review", LocalDate.now(), "funfact");
            r1.setUser_id(1L);
            r1.setProduct(p1);
            rRepo.save(r1);
        }
        if (pRepo.findBySku("c1d4e7r8d5f2").isEmpty()) {
            Product p2 = new Product("c1d4e7r8d5f2");
            pRepo.save(p2);
            Review r2 = new Review(2L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p2);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("c4d4f1v2f5v3").isEmpty()) {
            Product p3 = new Product("c4d4f1v2f5v3");
            pRepo.save(p3);
            Review r2 = new Review(3L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p3);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("v145dc2365sa").isEmpty()) {
            Product p4 = new Product("v145dc2365sa");
            pRepo.save(p4);
            Review r2 = new Review(4L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p4);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("fg54vc14tr78").isEmpty()) {
            Product p5 = new Product("fg54vc14tr78");
            pRepo.save(p5);
            Review r2 = new Review(5L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p5);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("12563dcfvg41").isEmpty()) {
            Product p6 = new Product("12563dcfvg41");
            pRepo.save(p6);
            Review r2 = new Review(6L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p6);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("vcg46578vf32").isEmpty()) {
            Product p7 = new Product("vcg46578vf32");
            pRepo.save(p7);
            Review r2 = new Review(7L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p7);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("vgb576hgb675").isEmpty()) {
            Product p8 = new Product("vgb576hgb675");
            pRepo.save(p8);
            Review r2 = new Review(8L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p8);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("unbjh875ujg7").isEmpty()) {
            Product p9 = new Product("unbjh875ujg7");
            pRepo.save(p9);
            Review r2 = new Review(9L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p9);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("u1f4f5e2d2xw").isEmpty()) {
            Product p10 = new Product("u1f4f5e2d2xw");
            pRepo.save(p10);
            Review r2 = new Review(10L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p10);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("j85jg76jh845").isEmpty()) {
            Product p11 = new Product("j85jg76jh845");
            pRepo.save(p11);
            Review r2 = new Review(11L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p11);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
        if (pRepo.findBySku("g4f7e85f4g54").isEmpty()) {
            Product p12 = new Product("g4f7e85f4g54");
            pRepo.save(p12);
            Review r2 = new Review(12L, 1, "approved", "review", LocalDate.now(), "funfact");
            r2.setProduct(p12);
            r2.setUser_id(1L);
            rRepo.save(r2);
        }
    }
}
