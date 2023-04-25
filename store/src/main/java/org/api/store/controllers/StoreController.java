package org.api.store.controllers;

import org.api.store.dto.StoreCreationRequest;
import org.api.store.services.StoreService;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import lombok.extern.slf4j.Slf4j;

@Slf4j
@RestController
@RequestMapping("api/s/store")
public record StoreController(StoreService storeService) {
	
	@PostMapping
	public void createStore(@RequestBody StoreCreationRequest storeRequest) {
		log.info("Creating store {}", storeRequest);
		storeService.createStore(storeRequest);
	}
}
