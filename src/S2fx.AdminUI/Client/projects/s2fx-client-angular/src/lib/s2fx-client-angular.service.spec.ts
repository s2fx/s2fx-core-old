import { TestBed } from '@angular/core/testing';

import { S2fxClientAngularService } from './s2fx-client-angular.service';

describe('S2fxClientAngularService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));

    it('should be created', () => {
        const service: S2fxClientAngularService = TestBed.get(S2fxClientAngularService);
        expect(service).toBeTruthy();
    });
});
