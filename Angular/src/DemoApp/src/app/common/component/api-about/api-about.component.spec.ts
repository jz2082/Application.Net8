import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApiAboutComponent } from './api-about.component';

describe('ApiAboutComponent', () => {
  let component: ApiAboutComponent;
  let fixture: ComponentFixture<ApiAboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ApiAboutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ApiAboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
