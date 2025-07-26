import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { PrimengModule } from '@common/primeng.module';

@Component({
  selector: 'pluralsight-setting',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule
  ],
  templateUrl: './setting.component.html',
  styleUrl: './setting.component.scss'
})
export class SettingComponent {
}
