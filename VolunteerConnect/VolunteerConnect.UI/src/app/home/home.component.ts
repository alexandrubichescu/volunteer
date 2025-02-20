import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../Services/category.service';
import { NgFor, NgIf } from '@angular/common';
import { CategoriesComponent } from "../features/categories/categories.component";

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [NgIf, CategoriesComponent],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  categories: any[] = []; // Array to store categories

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getAllCategories().subscribe({
      next: (data) => {
        this.categories = data; // Bind the response to the categories array
      },
      error: (err) => {
        console.error('Failed to load categories:', err);
      },
    });
  }
  
}
