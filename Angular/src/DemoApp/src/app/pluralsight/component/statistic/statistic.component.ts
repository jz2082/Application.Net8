import { CommonModule } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'pluralsight-statistic',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './statistic.component.html',
  styleUrl: './statistic.component.scss'
})
export class StatisticComponent implements OnInit {
  @Input() icon : string = '';
  @Input() label : string = '';
  @Input() value: string = '';
  @Input() colour: string = '';

  constructor() { 
  }

  ngOnInit(): void {
  }
}
