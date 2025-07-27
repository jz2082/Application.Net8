import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutComponent } from './about.component';

describe('AboutComponent', () => {
  let component: AboutComponent;
  let fixture: ComponentFixture<AboutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AboutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AboutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
describe('AboutComponent isAuthenticated', () => {
  let component: AboutComponent;

  beforeEach(() => {
    // Create a mock authService with isLoggedIn property
    const mockAuthService = {
      isLoggedIn: false
    };
    // Create AboutComponent instance with mock authService
    component = new AboutComponent();
    // @ts-ignore
    component.authService = mockAuthService;
  });

  it('should return true when authService.isLoggedIn is true', () => {
    // @ts-ignore
    component.authService.isLoggedIn = true;
    expect(component.isAuthenticated).toBeTrue();
  });

  it('should return false when authService.isLoggedIn is false', () => {
    // @ts-ignore
    component.authService.isLoggedIn = false;
    expect(component.isAuthenticated).toBeFalse();
  });
});
