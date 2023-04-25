package org.api.store.services;

import java.util.UUID;

import org.api.store.dto.StoreCreationRequest;
import org.api.store.models.Store;
import org.api.store.repositories.IStoreRepository;
import org.springframework.stereotype.Service;

@Service
public record StoreService(IStoreRepository storeRepo) {
	public void createStore(StoreCreationRequest dto) {
		Store store = Store.builder()
				.address(dto.address())
				.storeCode(UUID.randomUUID())
				.build();
		
		storeRepo.saveAndFlush(store);
	}
}
