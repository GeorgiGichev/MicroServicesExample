package org.store.services.data;

import java.util.UUID;

import org.store.data.models.Store;
import org.store.dto.StoreCreationRequest;

public record StoreService() {
	public void createStore(StoreCreationRequest dto) {
		Store store = Store.builder()
				.address(dto.address())
				.storeCode(UUID.randomUUID())
				.build();
	}
}
