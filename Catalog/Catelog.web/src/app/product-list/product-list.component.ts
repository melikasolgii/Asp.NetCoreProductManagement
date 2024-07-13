import { AfterViewInit,ChangeDetectorRef  , Component, ViewChild } from '@angular/core';
import { AppModule } from '../app.module';
import { Product } from '../models/product.model';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ProductService } from '../services/product.service';
import { consumerMarkDirty } from '@angular/core/primitives/signals';
import { ProductSearchRequest } from '../models/product-search-request';
import { FormControl } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { map, pipe } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material/dialog';
import { AddProductComponent } from '../add-product/add-product.component';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [AppModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss'
})

export class ProductListComponent implements AfterViewInit  {
 productDataSource : MatTableDataSource<Product>;
 columnsToDisplay :  string[];
@ViewChild(MatSort) sort : MatSort;
@ViewChild(MatPaginator) paginator : MatPaginator;
pageSizeOptions :number[];
defaultPageSize:number;
totalRecordCount:number;
searchText :FormControl;
loading:boolean;
isFirstLoad:boolean;
private totalRecordCountHeaderName :  string='x-totalrecordcount';

//hook (system event)
constructor(
  private productService:ProductService,
  private cdRef: ChangeDetectorRef,
  private toastrService: ToastrService,
  private dialogService :MatDialog)
  {

    //fake data

    //  let data :Product[] = [];
    //  new Product ("1" , "Mobile" , 100,"Sample Mobile"),
    //  new Product ("2" , "Mobile" , 200,"Sample Mobile"),
    // ];
// let data = productService.search();



let data :Product[] = [];
this.productDataSource = new MatTableDataSource<Product>(data);
// var request =new ProductSearchRequest('mobile','price desc',3,1);

// this.productService.search(request).subscribe({
//   next:( data:Product[] )=>{
// console.log(data);
// this.productDataSource = new MatTableDataSource(data);
//   },
//   error:(err:any)=>{
// console.log(err);
//   }
//    });


this.columnsToDisplay = ['Id', 'Name' ,'Price','Actions'];

this.productDataSource.sort = this.sort;

this.pageSizeOptions = [ 2 , 5 , 10 , 15 , 20] ;

this.defaultPageSize = 2;

this.totalRecordCount = 0;

this.searchText=new FormControl('');

this.loading=false;
this.isFirstLoad=true;


}


ngAfterViewInit (): void {
  this.loadProducts();

  //this one for duplication error
  this.cdRef.detectChanges();

  this.sort.sortChange.subscribe(() => {
    this.loadProducts();

  });

  this.paginator.page.subscribe(() => {
    this.loadProducts();

  });
}


loadProducts(){
  this.loading=true;
  var text = this.searchText.value;
  var sort = `${this.sort.active} ${this.sort.direction}`;
  let pageSize=this.paginator.pageSize;
   let pageIndex = this.paginator.pageIndex + 1;

  var request =new ProductSearchRequest(text,sort,pageSize,pageIndex);

  this.productService.search(request).pipe(map(Response=>
    {
     this.totalRecordCount=parseInt(Response.headers.get(this.totalRecordCountHeaderName)!);
     console.log(Response.headers.get(this.totalRecordCountHeaderName));
     return Response.body as Product[];
    }))
     .subscribe({
    next:(data:Product[])=>{
    console.log(data);
    this.productDataSource = new MatTableDataSource(data);
    this.loading=false;
    if(this.isFirstLoad)
      {
        this.toastrService.info('Loading Completed','Loading');
        this.isFirstLoad= false;
      }

    },
    error:(err:any)=>{
   console.log(err);
   this.loading=false;
   this.toastrService.error('Loading Failed','Loading');
    }
     });
}

search(){
  this.loadProducts();
}

add() {
  this.dialogService.open(AddProductComponent, {
    width: '30%'
  }).afterClosed().subscribe((result: boolean) => {
    if (result) {
      this.loadProducts();
    }
  });
}


}
