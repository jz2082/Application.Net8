import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import { LoggerService } from '@common/service/logger.service';
import { environment } from '@env/environment';
import { Product } from '@demo/model/product';
import { ApiResponse } from '@common/model/apiResponse';
import { AdditionalInfo } from '@common/model/additionalInfo';

describe('ProductService', () => {
    let service: ProductService;
    let httpMock: HttpTestingController;
    let loggerSpy: jasmine.SpyObj<LoggerService>;
    const apiBaseUrl = environment.appSetting.apiBaseUrl + 'api/Product';
    let mockProduct: Product = {
        id: 'testid',
        productId: 1,
        productName: 'Test',
        productCode: 'Test',
        price: 10,
        description: 'Test Description',
        starRating: 5,
        imageUrl: '',
        releaseDate: new Date('2025-01-01'),
        dateCreated: new Date('2024-01-01'),
        dateModified: new Date('2024-06-01'),
        rowVersion: []
    };

    beforeEach(() => {
        loggerSpy = jasmine.createSpyObj('LoggerService', ['logTrace', 'logDebug']);
        TestBed.configureTestingModule({
            imports: [HttpClientTestingModule],
            providers: [
                ProductService,
                { provide: LoggerService, useValue: loggerSpy }
            ]
        });
        service = TestBed.inject(ProductService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should get entity list', () => {
        const mockResponse: ApiResponse<Product[]> = { data: [], hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.getEntityList().subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(apiBaseUrl + '/list');
        expect(req.request.method).toBe('GET');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalled();
    });

    it('should get entity by id', () => {
        mockProduct = { ...mockProduct, productId: 1, productName: 'Test', price: 10 };
        const mockResponse: ApiResponse<Product> = { data: mockProduct, hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.getEntity(1).subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(`${apiBaseUrl}/entity/1`);
        expect(req.request.method).toBe('GET');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalled();
    });

    it('should create entity', () => {
        mockProduct = { ...mockProduct, productId: 2, productName: 'New', price: 20 };
        const mockResponse: ApiResponse<Product> = { data: mockProduct, hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.createEntity(mockProduct).subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(`${apiBaseUrl}/entity`);
        expect(req.request.method).toBe('POST');
        req.flush(mockResponse);
        expect(loggerSpy.logDebug).toHaveBeenCalled();
    });

    it('should update entity', () => {
        mockProduct = { ...mockProduct, productId: 3, productName: 'Update', price: 30 };
        const mockResponse: ApiResponse<Product> = { data: mockProduct, hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.updateEntity(mockProduct).subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(`${apiBaseUrl}/entity`);
        expect(req.request.method).toBe('PUT');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalled();
    });

    it('should delete entity', () => {
        mockProduct = { ...mockProduct, productId: 4, productName: 'Delete', price: 40 };
        const mockResponse: ApiResponse<Product> = { data: mockProduct, hasViolation: false, hasException: false, ruleViolation: new AdditionalInfo() };
        service.deleteEntity(4).subscribe(res => {
            expect(res).toEqual(mockResponse);
        });
        const req = httpMock.expectOne(`${apiBaseUrl}/entity/4`);
        expect(req.request.method).toBe('DELETE');
        req.flush(mockResponse);
        expect(loggerSpy.logTrace).toHaveBeenCalled();
    });
});
