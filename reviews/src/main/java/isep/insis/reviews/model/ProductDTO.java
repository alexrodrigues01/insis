package isep.insis.reviews.model;

import java.io.Serializable;

public class ProductDTO implements Serializable {
    private String sku;
    private String designation;

    private Long productId;

    public ProductDTO(String sku, String designation,Long productId) {
        this.sku = sku;
        this.designation = designation;
        this.productId=productId;
    }

    public ProductDTO(String sku) {
        this.sku = sku;
    }

    public String getSku() {
        return sku;
    }

    public void setSku(String sku) {
        this.sku = sku;
    }

    public String getDesignation() {
        return designation;
    }

    public void setDesignation(String designation) {
        this.designation = designation;
    }

    @Override
    public String toString() {
        return "ProductDTO{" +
                "sku='" + sku + '\'' +
                ", designation='" + designation + '\'' +
                ", productId=" + productId +
                '}';
    }
}