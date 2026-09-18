import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommonPanel } from './common-panel.component';

describe('CommonPanel', () => {
  let component: CommonPanel;
  let fixture: ComponentFixture<CommonPanel>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonPanel]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CommonPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
