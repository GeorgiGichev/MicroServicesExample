package org.storeService.dto;

import java.util.UUID;

public record StoreCreationRequest(UUID storeCode, String address) {
	
}
