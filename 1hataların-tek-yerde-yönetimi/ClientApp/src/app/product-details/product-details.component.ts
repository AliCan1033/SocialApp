import { Component, Input, OnInit } from '@angular/core';
import { Product } from '../Model';
import { ProductService } from '../product.service';

@Component({
  selector: 'product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {


  @Input() product: Product;
  @Input() products: Product[];


  constructor(private productService:ProductService) { }

  ngOnInit(): void {
  }

  addProduct(id:number,name:string,price:number,isActive:boolean){

    const p = new Product(id,name,price,isActive);
    this.productService
    .updateProduct(p)
    .subscribe(result => {
      this.products.splice(this.products.findIndex(x => x.productId==p.productId),1,p)
    });//splice= değiştirme findIndex=değişecek bilgiyi bulma bulduktan sonra bir tane kaydı güncelle sonra p objesni ekle
    this.product=null;

  }

}
