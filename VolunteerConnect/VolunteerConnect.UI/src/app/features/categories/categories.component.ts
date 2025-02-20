import { ChangeDetectorRef, Component, NgModule, OnInit } from '@angular/core';
import { CategoryService } from '../../Services/category.service';
import { AuthService } from '../../Services/auth.service';
import { NgFor, NgIf } from '@angular/common';
import { FormsModule} from '@angular/forms';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-categories',
  standalone: true,
  imports: [ NgFor, NgIf, FormsModule, RouterModule],
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
})
export class CategoriesComponent implements OnInit {
  categories: any[] = [];
  isAdmin: boolean = false;
  isAddCategoryModalVisible = false;
  newCategoryTitle = '';
  newDescription = '';
  newImageUrl = '';
  errorMessage: string | null = null;
  isLoggedIn: boolean = false;

  constructor(
    private categoryService: CategoryService,
    private authService: AuthService,
    private router: Router,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    
     // Subscribe to changes
     this.authService.isLoggedIn$.subscribe((loggedIn) => {
      this.loadCategories();
      this.isLoggedIn = this.authService.isAuthenticated();
      this.isAdmin = this.authService.getRole() === 'Admin'; // Check if the user is Admin
      this.isLoggedIn = loggedIn;
      this.cdr.detectChanges();
    });
  }
 

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data; // Bind the response to the categories array
        this.cdr.detectChanges();
        console.log('Categories category from loadcategories: ', this.categories);
      },
      error: (err) => {
        if (err.status === 401) {
          console.error('Unauthorized: Token is invalid or expired.');
          // Redirect to login page
          this.authService.clearTokenAndRole();
          this.router.navigate(['/login']);
        } else {
          console.error('Failed to load categories:', err);
        }
      },
    });
  }
  
  showAddCategoryModal(): void {
    this.isAddCategoryModalVisible = true;
  }

  closeAddCategoryModal(): void {
    this.isAddCategoryModalVisible = false;
  }

  addCategory(): void {
    if (!this.newCategoryTitle.trim() && !this.newDescription.trim() && !this.newImageUrl.trim()) {
      this.errorMessage = 'Category title is required.';
      return;
    }
  
    this.categoryService.createCategory(this.newCategoryTitle, this.newDescription, this.newImageUrl).subscribe({
      next: (response) => {
        if (response.success) {
          // Reload categories and close modal
          this.loadCategories(); // Refresh the categories list
          this.closeAddCategoryModal(); // Close the modal
          this.newCategoryTitle = ''; // Reset the input field
        } else {
          // Handle validation errors
          this.errorMessage = response.validationErrors.join(', ');
        }
      },
      error: (err) => {
        console.error('Failed to add category', err);
        this.errorMessage = 'An error occurred while adding the category.';
      },
    });
  }
}

