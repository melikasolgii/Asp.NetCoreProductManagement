import { Injectable } from '@angular/core';
import { Product } from '../models/product.model';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ProductSearchRequest } from '../models/product-search-request';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  productServiceUrl :string ='https://localhost:5001/Products';

  constructor(private http: HttpClient) { }

  //DTO() (Text, sort, pageIndex , pageSize)

//fake service
// search():Product[]
// {

//   let data :Product[] = [
//     new Product ("1" , "Mobile" , 100,"Sample Mobile"),
//     new Product ("2" , "Mobile" , 200,"Sample Mobile"),
//   ];

//   return data;
// }


 //DTO() (Text, sort, pageIndex , pageSize)
 search(request:ProductSearchRequest):Observable<HttpResponse<Product[]>>
{
// let endPointUrl =` ${this.productServiceUrl}/search?Text=${request.text}&Sort=${request.sort}&pageSize=${request.pageSize}&pageIndex=${request.pageIndex}`;

let queryParams=new HttpParams()
.set('text', request.text)
.set('sort',request.sort)
.set('pageSize', request.pageSize)
.set('pageIndex',request.pageIndex);

let endPointUrl =`${this.productServiceUrl}/search`;


return this.http.get<Product[]>(endPointUrl,{
  params:queryParams,
  observe:'response'
});

}


addProduct(product:Product): Observable<never> {
  return this.http.post<never>(this.productServiceUrl, product);
}


}
