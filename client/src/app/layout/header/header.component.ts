import { Component, effect, signal } from '@angular/core';
import { MatBadge } from '@angular/material/badge';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [MatIcon, MatButton, MatBadge],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent {
  themes = {
    lightTheme: 'light-theme',
    darkTheme: 'dark-theme',
  };

  theme = signal<string>(this.getThemeFromLocalStorage());

  ngOnInit(): void {}

  getThemeFromLocalStorage() {
    return localStorage.getItem('theme') || this.themes.lightTheme;
  }

  constructor() {
    effect(() => {
      document.documentElement.className = this.theme();
      localStorage.setItem('theme', this.theme());
    });
  }

  handleTheme() {
    const { lightTheme, darkTheme } = this.themes;
    const newTheme = this.theme() === lightTheme ? darkTheme : lightTheme;

    this.theme.set(newTheme);
  }
}
