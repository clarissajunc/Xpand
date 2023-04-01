import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditPlanetDialogComponent } from './edit-planet-dialog.component';

describe('EditPlanetDialogComponent', () => {
  let component: EditPlanetDialogComponent;
  let fixture: ComponentFixture<EditPlanetDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditPlanetDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditPlanetDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
