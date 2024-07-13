import { Component } from '@angular/core';
import { AppModule } from '../app.module';
import { MatDialogRef } from '@angular/material/dialog';
import { AbstractControl, FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { ToastrService } from 'ngx-toastr';
import { Product } from '../models/product.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-add-product',
  standalone: true,
  imports: [AppModule],
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent
{
  addProductForm: FormGroup;

  constructor(
    private dialogServiceRef : MatDialogRef<AddProductComponent> ,
    private productService: ProductService,
    private toastrService: ToastrService)
  {
    this.addProductForm=new FormGroup({
     name:new FormControl('', [Validators.required,Validators.minLength(3),Validators.maxLength(100)]),
     price:new FormControl('', [Validators.required,Validators.pattern('\\d+')]),
    //  price: new FormControl('', [
    //   Validators.required,
    //   Validators.pattern('^[0-9]+$') // updated pattern to match only digits
    // ]),
     description :new FormControl(''),
    });  }


    // save() {
    //   console.log('save method called');
    //   if (this.addProductForm.invalid) {
    //     console.log('Form is invalid');
    //     this.toastrService.error('Product information is invalid', 'Add Product');
    //     return;
    //   }

    //   let product: Product = this.addProductForm.value as Product;
    //   console.log('Product object:', product);

    //   this.productService.addProduct(product).subscribe({
    //     next: () => {
    //       console.log('Product added successfully');

    //       this.toastrService.success('Save record successfully', 'Save');
    //       this.dialogServiceRef.close(true);
    //     },
    //     error: (err: any) => {
    //       console.log('Error adding product:', err);

    //       this.toastrService.error('Save record failed', 'Save');
    //     }
    //   });
    // }

    save() {
      console.log('save method called');
      console.log('Form values:', this.addProductForm.value);
      console.log('Form errors:', this.addProductForm.errors);

      // if (this.addProductForm.invalid) {
      //   console.log('Form is invalid');
      //   this.toastrService.error('Product information is invalid', 'Add Product');
      //   return;
      // }

      let product: Product = this.addProductForm.value as Product;
      console.log('Product object:', product);

      this.productService.addProduct(product).subscribe({
        next: (result) => {
          console.log('Product added successfully:', result);
          this.toastrService.success('Save record successfully', 'Save');
          this.dialogServiceRef.close(true);
        },
        error: (err: any) => {
          console.log('Error adding product:', err);
          this.toastrService.error('Save record failed', 'Save');
        }
      });
    }

  cancel()
  {
    this.dialogServiceRef.close(false);
  }

  get name():FormControl
  {
  return this.addProductForm.get('name')as FormControl;
  }

  get price():FormControl
  {
    return this.addProductForm.get('price')as FormControl;
  }


    getNameError(): string {
      if (this.name.hasError('required')) {
        return 'Name is a required field';
      }

      if (this.name.hasError('minlength')) {
        return 'Name should be a minimum 3 characters';
      }

      if (this.name.hasError('maxlength')) {
        return 'Name should be a maximum 100 characters';
      }
      return '';
    }

  getPriceError(): string {
    if (this.price.hasError('required'))
    {
      return 'Price is a required field';
    }
    if (this.price.hasError('pattern'))
    {
      return 'price is a numeric value';
    }
    return '';
  }




}
