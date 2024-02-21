package isep.insis.products.controllers;

import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import isep.insis.products.Runner;
import isep.insis.products.model.Product;
import isep.insis.products.model.ProductDTO;
import isep.insis.products.services.ProductService;
import isep.insis.products.utils.EventDTO;
import isep.insis.products.utils.TypeOfEvent;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;



import org.springframework.web.bind.annotation.RequestBody;


@Tag(name = "Product", description = "Endpoints for managing  products")
@RestController
@RequestMapping("/products")
class ProductController {
    private static final Logger logger = LoggerFactory.getLogger(ProductController.class);

    @Autowired
    private Runner runner;
    @Autowired
    private ProductService service;

    @Operation(summary = "creates a product")
    @PostMapping
    @ResponseStatus(HttpStatus.CREATED)
    public ResponseEntity<ProductDTO> create(@RequestBody Product manager) {
        try {
            final ProductDTO product = service.create(manager);
            runner.sendMessage(new EventDTO(TypeOfEvent.CREATE,"Product",product));
            return new ResponseEntity<ProductDTO>(product, HttpStatus.CREATED);

        }
        catch( Exception e ) {
            System.out.println(e.getMessage());
            throw new ResponseStatusException(HttpStatus.CONFLICT,"Product must have a unique SKU.");
        }
    }

    @Operation(summary = "updates a product")
    @PatchMapping(value = "/{sku}")
    public ResponseEntity<ProductDTO> Update(@PathVariable("sku") final String sku, @RequestBody final Product product) {

        final ProductDTO productDTO = service.updateBySku(sku, product);

        if( productDTO == null )
            throw new ResponseStatusException(HttpStatus.NOT_FOUND,"Product not found.");
        else {
            try {
                runner.sendMessage(new EventDTO(TypeOfEvent.UPDATE, "Product", productDTO));
            } catch (Exception e) {
                throw new RuntimeException(e);
            }
            return ResponseEntity.ok().body(productDTO);
        }
    }

    @Operation(summary = "deletes a product")
    @DeleteMapping(value = "/{sku}")
    public ResponseEntity<Product> delete(@PathVariable("sku") final String sku ){

        service.deleteBySku(sku);
        try {
            runner.sendMessage(new EventDTO(TypeOfEvent.DELETE,"Product",new ProductDTO(sku,null,null,null)));
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
        return ResponseEntity.noContent().build();
    }

}