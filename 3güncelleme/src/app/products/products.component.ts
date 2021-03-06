import { Component, OnInit } from '@angular/core';
import { Model, Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  products: Product[];
  selectedProduct: Product;

  constructor(private productService: ProductService) { } //ProductService injek ediyoruz

  ngOnInit(): void {
    this.products=this.productService.getProducts();//getProducts() produc.serviceye ekleyerek genel kullanılabilir hale getirdik
  }

  onSelectProduct(product:Product){
    this.selectedProduct=product;
  }

}
