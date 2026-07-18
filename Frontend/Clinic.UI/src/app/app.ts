import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MainLayoutComponent } from './core/layout/main-layout/main-layout.component';

@Component({
  selector: 'app-root',
  imports: [MainLayoutComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
})
export class App {}
