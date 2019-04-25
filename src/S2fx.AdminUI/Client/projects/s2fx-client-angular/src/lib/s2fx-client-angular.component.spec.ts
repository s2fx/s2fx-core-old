import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { S2fxClientAngularComponent } from './s2fx-client-angular.component';

describe('S2fxClientAngularComponent', () => {
    let component: S2fxClientAngularComponent;
    let fixture: ComponentFixture<S2fxClientAngularComponent>;

    beforeEach(async(() => {
        TestBed.configureTestingModule({
            declarations: [S2fxClientAngularComponent]
        })
            .compileComponents();
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(S2fxClientAngularComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
