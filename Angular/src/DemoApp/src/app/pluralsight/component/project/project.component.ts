import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { PrimengModule } from '@common/primeng.module';

import { AppInjectorService } from '@app/common/service/app-injector.service';
import { ControlMessageComponent } from '@app/common/component/control-message.component';

@Component({
  selector: 'pluralsight-project',
  standalone: true,
  imports: [
    CommonModule,
    PrimengModule,  
    ControlMessageComponent
  ],
  schemas: [ 
    CUSTOM_ELEMENTS_SCHEMA 
  ],
  templateUrl: './project.component.html',
  styleUrl: './project.component.scss'
})
export class ProjectComponent implements OnInit {
  city: string = '';
  projectForm: FormGroup;
  minProjectDate = new Date();
  allDevs = [
    { label: 'Jill', value: 'Jill Cool' },
    { label: 'Joe', value: 'Joe Cool' },
    { label: 'Mary', value: 'Mary Cool' },
    { label: 'Susan', value: 'Susan Jones' },
    { label: 'Phil', value: 'Phil Stephens' },
    { label: 'Karen', value: 'Karen Phillips' },
    { label: 'Chris', value: 'Chris Hampton' },
    { label: 'Si', value: 'Si Chew' },
    { label: 'Terri', value: 'Terri Smith' }
  ]

  constructor(private fb: FormBuilder) {
    const injector = AppInjectorService.getInjector();
    this.fb = injector.get(FormBuilder);
    this.projectForm = this.fb.group({
      projectId: ['', [Validators.required, Validators.minLength(5)]],
      description: ['My cool project', [Validators.required, Validators.maxLength(140)]],
      startDate: [new Date(), Validators.required],
      projectType: ['B'],
      selectedDevs: [[]],
      rating: [3]
    })
  }

  ngOnInit(): void {
  }

  hasFormErrors() {
    return !this.projectForm.valid;
  }

  onSubmit() {
    alert(JSON.stringify(this.projectForm.value));
  }
}