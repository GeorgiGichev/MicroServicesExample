package org.storeService.services;

import java.util.UUID;

import org.springframework.stereotype.Service;
import org.storeService.dto.StoreCreationRequest;
import org.storeService.models.Store;
import org.storeService.repositories.IStoreRepository;

@Service
public record StoreService(IStoreRepository storeRepo) {
	public void createStore(StoreCreationRequest dto) {
		Store store = Store.builder()
				.address(dto.address())
				.storeCode(UUID.randomUUID())
				.build();
		
		storeRepo.save(store);
	}
}
