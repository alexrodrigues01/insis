//package isep.insis.reviews.bootstrapper;
//
//import isep.insis.reviews.model.Product;
//import isep.insis.reviews.repositories.ProductRepository;
//import org.springframework.beans.factory.annotation.Autowired;
//import org.springframework.boot.CommandLineRunner;
//import org.springframework.stereotype.Component;
//
//@Component
////@Profile("bootstrap")
//public class ProductBootstrapper implements CommandLineRunner {
//
//    @Autowired
//    private ProductRepository pRepo;
//
//    @Override
//    public void run(String... args) throws Exception {
//
//        if (pRepo.findBySku("asd578fgh267").isEmpty()) {
//            Product p1 = new Product("asd578fgh267");
//            pRepo.save(p1);
//        }
//        if (pRepo.findBySku("c1d4e7r8d5f2").isEmpty()) {
//            Product p2 = new Product("c1d4e7r8d5f2");
//            pRepo.save(p2);
//        }
//        if (pRepo.findBySku("c4d4f1v2f5v3").isEmpty()) {
//            Product p3 = new Product("c4d4f1v2f5v3");
//            pRepo.save(p3);
//        }
//        if (pRepo.findBySku("v145dc2365sa").isEmpty()) {
//            Product p4 = new Product("v145dc2365sa");
//            pRepo.save(p4);
//        }
//        if (pRepo.findBySku("fg54vc14tr78").isEmpty()) {
//            Product p5 = new Product("fg54vc14tr78");
//            pRepo.save(p5);
//        }
//        if (pRepo.findBySku("12563dcfvg41").isEmpty()) {
//            Product p6 = new Product("12563dcfvg41");
//            pRepo.save(p6);
//        }
//        if (pRepo.findBySku("vcg46578vf32").isEmpty()) {
//            Product p7 = new Product("vcg46578vf32");
//            pRepo.save(p7);
//        }
//        if (pRepo.findBySku("vgb576hgb675").isEmpty()) {
//            Product p8 = new Product("vgb576hgb675");
//            pRepo.save(p8);
//        }
//        if (pRepo.findBySku("unbjh875ujg7").isEmpty()) {
//            Product p9 = new Product("unbjh875ujg7");
//            pRepo.save(p9);
//        }
//        if (pRepo.findBySku("u1f4f5e2d2xw").isEmpty()) {
//            Product p10 = new Product("u1f4f5e2d2xw");
//            pRepo.save(p10);
//        }
//        if (pRepo.findBySku("j85jg76jh845").isEmpty()) {
//            Product p11 = new Product("j85jg76jh845");
//            pRepo.save(p11);
//        }
//        if (pRepo.findBySku("g4f7e85f4g54").isEmpty()) {
//            Product p12 = new Product("g4f7e85f4g54");
//            pRepo.save(p12);
//        }
//    }
//}
