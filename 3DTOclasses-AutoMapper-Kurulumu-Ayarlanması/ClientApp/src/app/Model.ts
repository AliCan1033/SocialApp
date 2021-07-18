

export class Model{
  products;
  constructor(){
    // this.products=[
    //   new Product (1,"samsungs5",2000,false),
    //   new Product (2,"samsungs6",2000,false),
    //   new Product (3,"samsungs7",2000,true),
    //   new Product (4,"samsungs8",2000,true),
    //   new Product (5,"samsungs9",2000,true),
    //   new Product (6,"samsungs10",2000,true)
    // ]
  }
}
export class Product {
  productId:number;
  name:string;
  price:number;
  isActive:boolean;
  constructor(productId:number,name:string,price:number,isActive:boolean) {
    this.productId=productId;
    this.name=name;
    this.price=price;
    this.isActive=isActive;
  }
}
