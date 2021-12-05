<script>
	import { createEventDispatcher } from 'svelte';

	export let open = false;
	export let showCloseButton = false;
	const dispatch = createEventDispatcher();

</script>

{#if open}
	<div class='modal z-50 fixed w-full h-full top-0 left-0 flex items-center justify-center p-8 lg:p-0'>
		<div class='modal-overlay fixed w-full h-full bg-gray-900 opacity-75' on:click={() => dispatch('close')}></div>
		<div class='bg-white lg:h-max w-120  mx-auto rounded-lg shadow-xl z-50 overflow-y-auto'>
			{#if showCloseButton}
				<div class='text-right'>
					<button type='button' class='p-2 bg-gray-200 hover:bg-gray-300 rounded-full'
									on:click={() => dispatch('close')}>
						<svg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' class='w-5 h-5'>
							<path d='M0 0h24v24H0V0z' fill='none' />
							<path
								d='M19 6.41L17.59 5 12 10.59 6.41 5 5 6.41 10.59 12 5 17.59 6.41 19 12 13.41 17.59 19 19 17.59 13.41 12 19 6.41z' />
						</svg>
					</button>
				</div>
			{/if}
			<slot name='body' />
		</div>
	</div>
{/if}
