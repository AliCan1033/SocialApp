import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {

  @Input() products: Product[];
  constructor(private productService: ProductService) { }

  ngOnInit(): void {
  }
  addProduct(name:string,price:number,isActive:boolean){
    console.log(name);
    console.log(price);
    console.log(isActive);

    const p = new Product(0,name,price,isActive);
    this.productService.addProduct(p).subscribe(product =>{
      this.products.push(product);//inputtan gelen bilgiyi product üzerindeki liste üzerine eklemiş oluyoruz
    });
  }
}
