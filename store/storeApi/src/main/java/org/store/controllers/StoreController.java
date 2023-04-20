package org.store.controllers;

import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.store.dto.StoreCreationRequest;
import org.store.services.data.StoreService;

import lombok.extern.slf4j.Slf4j;

@Slf4j
@RestController
@RequestMapping("api/s/store")
public record StoreController(StoreService storeService) {
	
	@PostMapping
	public void createStore(@RequestBody StoreCreationRequest storeRequest) {
		System.out.println(String.format("New store creation %", storeRequest.address()));
		storeService.createStore(storeRequest);
	}
}
